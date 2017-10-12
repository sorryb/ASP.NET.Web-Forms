using System;

namespace BugsTrackers.BusinessLogicLayer {



	//*********************************************************************
	//
	// CategoryTree Class
	//
	// The CategoryTree class contans methods for generating the tree
	// of categories displayed by the PickCategory control.
	//
	//*********************************************************************

    public class CategoryTree {

        private int _CatIndent = 1;
        private CategoryCollection _UnSortedCats;
        private CategoryCollection _SortedCats;


        public CategoryCollection GetCategoryTreeByProjectId(int projectId) {
            _SortedCats = new CategoryCollection();
            _UnSortedCats = Category.GetCategoryByProjectId(projectId);
            foreach(Category parentCat in GetTopLevelCategories() ) {
                _SortedCats.Add( parentCat );
                BindSubCategories(parentCat.Id);
            }
            return _SortedCats;
        }


        void BindSubCategories(int parentId) {
            foreach(Category childCat in GetChildCategories(parentId) ) {
                _SortedCats.Add( new Category( DisplayIndent() + childCat.Name, childCat.Id ) );
                _CatIndent ++;
                BindSubCategories(childCat.Id);
                _CatIndent --;
            }
        }



        CategoryCollection GetTopLevelCategories() {
            CategoryCollection colCats = new CategoryCollection();
            foreach (Category cat in _UnSortedCats)
                if (cat.ParentCategoryId == 0)
                    colCats.Add(cat);
            return colCats;
        }


        CategoryCollection GetChildCategories(int parentId) {
            CategoryCollection colCats = new CategoryCollection();
            foreach (Category cat in _UnSortedCats)
                if (cat.ParentCategoryId == parentId)
                    colCats.Add(cat);
            return colCats;
        }


        string DisplayIndent() {
            return new String('-', _CatIndent) + " ";
        }




    }
}
