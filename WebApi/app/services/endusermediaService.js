app.service('endusermediaService', function ($http) {

    ////Get Single Records  
    //this.getone = function (UpdateID) {
    //    return $http.get("/api/UpdateOne/" + UpdateID);
    //}

    //Get All Update of a product
    this.getAllMedia = function (UpdateID) {

        var ab = $http.get("/api/Media?id=" + UpdateID);
        return ab;
    }

});
