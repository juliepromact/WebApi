app.service('updateService', function ($http) {

    ////Get Single Records  
    //this.getone = function (UpdateID) {
    //    return $http.get("/api/UpdateOne/" + UpdateID);
    //}

    //Get All Update of a product
        this.getAllUpdate = function (ProductID) {
            
            var ab=$http.get("/api/EndUserUpdate?id=" + ProductID);
            return ab;
        }

    //Create new update 
        this.post = function (ProductID,Update) {
            var request = $http({
                method: "post",
                url: "/api/Update/"+ProductID,
                data: Update
            });
            return request;
        }


    //Update the update "pun" 
        this.put = function (UpdateID, Update) {
            var request = $http({
                method: "put",
                url: "/api/Update/" + UpdateID,
                data: Update
            });
            return request;
        }

    //Delete the Record  
        this.delete = function (UpdateID) {
            var request = $http({
                method: "delete",
                url: "/api/Update/" + UpdateID
            });
            return request;
        }

        this.addmedia = function (UpdateID) {
            var request = $http({
                method: "get",
                url: "/api/Media/" + UpdateID
            });
            return request;
        }

});
