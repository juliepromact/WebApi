app.service('mediaService', function ($http) {

    ////Get Single Records  
    //this.getone = function (UpdateID) {
    //    return $http.get("/api/UpdateOne/" + UpdateID);
    //}

    //Get All Update of a product
    this.getAllMedia = function (UpdateID) {

        var ab = $http.get("/api/Media?id=" + UpdateID);
        return ab;
    }

    //Create new media 
    this.post = function (UpdateID, Media) {
        var request = $http({
            method: "post",
            url: "/api/Media/" + UpdateID,
            data: Media
        });
        return request;
    }

    //Update the media
    this.put = function (MediaID, Media) {
        var request = $http({
            method: "put",
            url: "/api/Media/" + MediaID,
            data: Media
        });
        return request;
    }

    //Delete the media  
    this.delete = function (MediaID) {
        var request = $http({
            method: "delete",
            url: "/api/Media/" + MediaID
        });
        return request;
    }

});
