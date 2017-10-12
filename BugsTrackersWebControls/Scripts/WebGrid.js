    var g_selectedRow,g_className;


    function Start() {
        alert('webGrid');
    }
    
    ///<summary>
	///Select Grid Row.
	///</summary>
	///<returns>void</returns>
    function SelectGridRow(obj,rowID)
    {
        //alert(rowID );
        var table = GetHiddenColumns(obj);
        divHolder.innerHTML = '';
        divHolder.appendChild(table) ;
        
        RemoveSelectedLine();
        
        g_selectedRow = obj.parentElement.parentElement;        
        g_className = obj.parentElement.parentElement.className.replace('hover','');
        
        obj.parentElement.parentElement.onmouseover='event.returnValue=false';
        obj.parentElement.parentElement.className = 'select';
        obj.parentElement.parentElement.onmouseout='event.returnValue=false';
            
    }
    ///<summary>
	///Remove Selected Line.
	///</summary>
	///<returns>void</returns>
    function RemoveSelectedLine()
    {
        objLine = g_selectedRow;
        
        if(objLine)
        {
            if(g_className == 'normal')
            {
                objLine.onmouseout = setOutNormal;
                objLine.onmouseover = setOverNormal;
            }
            else
            {
                objLine.onmouseout = setOutAlternate;
                objLine.onmouseover = setOverAlternate;        
            }
            objLine.className = g_className;
        }

    }
    ///<summary>
	///Set on mouse Over Normal.
	///</summary>
	///<returns>void</returns>
    function setOverNormal()
    {
        event.srcElement.parentElement.className = 'normalhover';
    }
    ///<summary>
	///Set on mouse Out Normal.
	///</summary>
	///<returns>void</returns>    
    function setOutNormal()
    {
        event.srcElement.parentElement.className = 'normal';
    }
    ///<summary>
	///Set on mouse Over Alternate.
	///</summary>
	///<returns>void</returns>
    function setOverAlternate()
    {
        event.srcElement.parentElement.className = 'alternatehover';
    }
    ///<summary>
	///Set on mouse Out Alternate.
	///</summary>
	///<returns>void</returns>
    function setOutAlternate()
    {
        event.srcElement.parentElement.className = 'alternate';
    }
////////////////////////////////////////////////////////////
    ///<summary>
	///Get Hidden Columns.
	///</summary>
	///<param name="rowCount"></param>
	///<returns>void</returns>
    function GetHiddenColumns(objImg)
    {
        var objLine = objImg.parentElement.parentElement.cells;
        var iCount = objImg.parentElement.parentElement.cells.length;
        
        var j = 0;
        var hiddenCols = new Array();
        for(var i=0;i<iCount;i++)
        {
            if(objLine[i].className == 'hiddenCell')
               { 
                var columnName = objImg.parentElement.parentElement.parentElement.rows[0].cells[i].children[0].innerHTML.bold() +  ' : '         
                hiddenCols[j] =  columnName +  objLine[i].innerHTML ;
                
                j++;
               }
         }
        var rowNumber,cellNumber;
        var CELL_NUMBER_ON_ROW = 3;
        if(hiddenCols.length > 3)
        {
            rowNumber = Math.ceil(hiddenCols.length / CELL_NUMBER_ON_ROW);            
        }
        else
            rowNumber = 1
        
        cellNumber = CELL_NUMBER_ON_ROW;
        
        var table = new Table(rowNumber,cellNumber,hiddenCols);
        table.width = '100%';
        return table;

    }
        //////////////////////table creator/////////////////////////
    ///<summary>
	///Table object.
	///</summary>
	///<param name="rowCount"></param>
	///<returns>void</returns>
    function Table(rowCount, colCount, contentArray)
    {
      this.width = '';
      this.height = '';	
      this.cellsContentArray = contentArray;  
    this.AddRow = function (srcTable)
    {
	    if(srcTable != null)
	    {
		    return srcTable.insertRow();
	    }
	    else
	    {
		    alert("Error while creating table. Cause: Container Table is null!");
	    }
    }

    this.AddCell = function (srcRow)
    {
	    if(srcRow != null)
	    {
		    return srcRow.insertCell();
	    }
	    else
	    {
		    alert("Error while creating table. Cause: Container row is null!");
	    }
    }

    this.IsValidNumber = function (ipNum)
    {
	    if(isNaN(ipNum))
	    {
		    alert("Invalid Number!");
		    return false;
	    }
	    else if(ipNum < 1)
	    {
		    alert("Number should be greater than 0!");
		    return false;
	    }
	    else
	    {
	    return true;
	    }
    }

    if(this.IsValidNumber(rowCount) && this.IsValidNumber(colCount) )
	    {		
		    var srcTable = document.createElement("table");
		    srcTable.className="mytable";
		    srcTable.cellSpacing = 3;
		    srcTable.height = this.height;
		    srcTable.width = this.width;
		    var tmpRow = null;
		    var tmpCell = null;
		    var cellsPerRow = 0;
		    var currentCell = 0;
    				
		    for(i=0; i<rowCount; i++)
		    {
			    tmpRow = this.AddRow(srcTable)
			    for(j=0; j<colCount; j++)
			    {
				    currentCell = cellsPerRow + j;			    
				    tmpCell = this.AddCell(tmpRow);
				    tmpCell.innerHTML = this.cellsContentArray[currentCell] != undefined?this.cellsContentArray[currentCell]:'';
				    tmpCell = null;
			    }
			    cellsPerRow = currentCell + 1;			
			    tmpRow = null;
		    }
		    return srcTable;
	    }



    }
    ///<summary>
	///Append Table.
	///</summary>
	///<param name="aspCheckBoxID"></param>
	///<returns>void</returns> 
    function AppendTable()
    {
       var srcTable = CreateTable(txtRows.value, txtCols.value);
       if((divHolder != null) && (divHolder.canHaveChildren))
       divHolder.appendChild(srcTable);
    }
///////////////////////////////////////////////////////////////////////////////
// this functions must be cached into a file JS - sorin burt comments to be replaced after

    ///<summary>
	///Check Objects.
	///</summary>
	///<param name="aspCheckBoxID"></param>
	///<returns>void</returns> 
    function CheckObjects()
    {
        try
        {
	        var objTbl = dataGridObj;//document.getElementById('WebGrid1_MyDataGrid');
	        var strHidden = HiddenSelectedCreditsObj.value;
    		
	        for(var i = 0;i<objTbl.rows.length;i++)
	        {
	        if(objTbl.rows[i].cells[2])
		        if(objTbl.rows[i].cells[2].innerHTML != '')
		        if(strHidden.indexOf(objTbl.rows[i].cells[2].innerHTML) >= 0 )
			        objTbl.rows[i].cells[1].childNodes[0].checked = true;	
		        else
			        objTbl.rows[i].cells[1].childNodes[0].checked = false;		

    			
	        }
        }
        catch(e)
        {
	        //alert(e.message)
        }
    }
    ///<summary>
	///Displayall checkbox from lines.
	///</summary>
	///<param name="aspCheckBoxID"></param>
	///<returns>void</returns> 
	function DisplayCheckedLines()
	{
		try
		{
		
			var strHiddenArr = HiddenSelectedCreditsObj.value.split(',');
			if(strHiddenArr.length > 0)
			{
				for(var i=0;i < strHiddenArr.length; i++)
				{
					CheckObjects(strHiddenArr[i])
				}
				
			}

		}
		catch(e){}
	}	
	///<summary>
	///Check all checkbox from lines.
	///</summary>
	///<param name="aspCheckBoxID"></param>
	///<returns>void</returns> 	
  function CheckAll(aspCheckBoxID, checkVal)
   {
  
        re = new RegExp(aspCheckBoxID);  //+ '$' //generated control name starts with a colon

		HiddenSelectedCreditsObj.value='';
		
        for(i = 0; i < document.forms[0].elements.length; i++)
         {

            elm = document.forms[0].elements[i];

            if (elm.type == 'checkbox')
            {				
                if (re.test(elm.name)) 
                {

                    elm.checked = checkVal;
                    if (checkVal==true)
						HiddenSelectedCreditsObj.value += elm.parentElement.parentElement.cells[elm.parentElement.cellIndex+1].innerText + ',' ;

                }
            }
        }
    }
	///<summary>
	///Check Line.
	///</summary>
	///<param name="objLine"></param>
	///<returns>void</returns>    
	function CheckLine(objchk)
	{
	
	 if(objchk.checked) 
		HiddenSelectedCreditsObj.value += objchk.parentElement.parentElement.cells[objchk.parentElement.cellIndex+1].innerText + ',' ;
	 else 
		{
			var strHidden = HiddenSelectedCreditsObj.value;  
			var nrFile =  objchk.parentElement.parentElement.cells[objchk.parentElement.cellIndex+1].innerText + ',';  
			HiddenSelectedCreditsObj.value = strHidden.replace(nrFile,'');  
			
		}
	}
	///<summary>
	///Change Color On Mouse Enter.
	///</summary>
	///<param name="objLine"></param>
	///<returns>void</returns>	
	function ChangeColorOnMouseEnter(objLine)
	{
	
	    if (objLine.style.backgroundColor =='#eeeeee' || objLine.style.backgroundColor =='#dddddd') 
	        objLine.style.backgroundColor='#cccccc'; 
	    else 
	        objLine.bgColor='#ffff33';
	
	}
	///<summary>
	///Change Color On Mouse Out.
	///</summary>
	///<param name="objLine"></param>
	///<returns>void</returns>
	function ChangeColorOnMouseOut(objLine)
	{
	    if (objLine.style.backgroundColor =='#cccccc') 
	     objLine.style.backgroundColor='#dddddd'; 
	    else 
	     objLine.bgColor='#ffff99'
	}
	
	///<summary>
	///Submit form.
	///</summary>
	///<param name="fileNumber"></param>
	///<param name="link"></param>
	///<returns>void</returns>
	function submitForm(fileNumber, link)
	{
	    //alert(fileNumber + '  ' + link);
	    var inputFileNumber = document.createElement("input");
	    inputFileNumber.id = 'FileNumber';
	    inputFileNumber.type = 'hidden';	    
	    document.forms[0].appendChild(inputFileNumber);
		document.getElementById('FileNumber').value = fileNumber;
	    var inputInitialPage = document.createElement("input");
	    inputInitialPage.id = 'initialPage';
	    inputInitialPage.type = 'hidden';
	    document.forms[0].appendChild(inputInitialPage);		
		document.getElementById('initialPage').value = document.forms[0].id;
		document.forms[0].__VIEWSTATE.name = 'NOVIEWSTATE';
		document.forms[0].action = link;
		document.forms[0].submit();
	}