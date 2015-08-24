app.service('signuprequestService', function ($http) {

    //Create new record  
    this.post = function (ProductOwner) {
        var request = $http({
            method: "post",
            url: "/api/EveryBody",
            data: ProductOwner
        });
        return request;
    }

    //Get Single Records  
    this.get = function (ID) {
        return $http.get("/api/EveryBody/" + ID);
    }

    //Get All Product  
    this.getAllSignupRequest = function () {
        return $http.get("/api/EveryBody");
    }

    //Update the Record  
    this.put = function (ID, ProductOwner) {
        var request = $http({
            method: "put",
            url: "/api/EveryBody/" + ID,
            data: ProductOwner
        });
        return request;
    }

    //Delete the Record  
    this.delete = function (ID) {
        var request = $http({
            method: "delete",
            url: "/api/EveryBody/" + ID
        });
        return request;
    }

    this.approve = function (ID) {
        var request = $http({
            method: "approve",
            url: "/api/EveryBody/" + ID
        });
        return request;
    }

  
});