function Start() {
    alert('JScript');
}


function include_dom(script_filename) {
    var html_doc = document.getElementsByTagName('head').item(0);
    var js = document.createElement('script');
    js.setAttribute('language', 'javascript');
    js.setAttribute('type', 'text/javascript');
    js.setAttribute('src', script_filename);
    html_doc.appendChild(js);
    return false;
}

//include_dom("http://ajax.googleapis.com/ajax/libs/jquery/1.2.6/jquery.min.js");