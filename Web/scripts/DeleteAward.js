(function () {
    var del = document.getElementById("delete");
    del.onclick = function () {
        if(confirm("Delete awards?")){
        return true;
    }
        else return false;
    }
})();