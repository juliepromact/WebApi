//app.controller('productController',['$scope','CRUD_OperService','$log', function ($scope, CRUD_OperService,$log) {
app.controller('enduserController', function ($scope, enduserService,$log) {
    $scope.OperType = 1;
   // var abc = [];
    //1 Mean New Entry  

    GetAllRecords();
    //To Get All Records  
    function GetAllRecords() {
        var promiseGet = enduserService.getAllProduct();
        promiseGet.then(function (pl) {
            $scope.EndUser = pl.data
        },
              function (errorPl) {
                  $log.error('Some Error in Getting Records.', errorPl);
              });
    }

    ////To Clear all input controls.  
    //function ClearModels() {
    //    $scope.OperType = 1;
    //    $scope.ProductID = "";
    //    $scope.ProductName = "";
    //}

    //To Create new record and Edit an existing Record.  
    $scope.save = function (endUserDetails) {
      
        $scope.abc = [];
        for (i in endUserDetails) {
            $scope.abc.push({
                ProductName: endUserDetails[i].productName,
                Assigned: endUserDetails[i].assigned,
                ProductID: endUserDetails[i].productID
            });
        }
        //var EndUser = {
        //    ProductName: $scope.ProductName,
          
        //};
        ////   if ($scope.OperType === 1) {
        var promisePost = enduserService.post($scope.abc);
        promisePost.then(function (pl) {
          
            $scope.Message = "Your Successfuly Followed Products";
            GetAllRecords();
            //   ClearModels();
        }, function (err) {
            console.log("Err" + err);
        });
        //} else {
        //    //Edit the record                
        //    EndUser.ProductID = $scope.ProductID;
        //    var promisePut = enduserService.put($scope.ProductID, EndUser);
        //    promisePut.then(function (pl) {
        //        $scope.Message = "Product Updated Successfuly";
        //        GetAllRecords();
        //      //  ClearModels();
        //    }, function (err) {
        //        console.log("Err" + err);
        //    });
        //}
    };

    ////To Delete Record  
    //$scope.delete = function (ProductNew) {
    //    var promiseDelete = CRUD_OperService.delete(ProductNew.productID);
    //    promiseDelete.then(function (pl) {
    //        $scope.Message = "Product Deleted Successfuly";
    //        GetAllRecords();
    //        ClearModels();
    //        //     $location.path('/login?id=' + ProductNew.productID);
    //    }, function (err) {
    //        console.log("Err" + err);
    //    });
    //}

    ////To Get Student Detail on the Base of Student ID  
    //$scope.get = function (ProductNew) {
    //    var promiseGetSingle = CRUD_OperService.get(ProductNew.productID);
    //    promiseGetSingle.then(function (pl) {
    //        var res = pl.data;
    //        $scope.ProductID = res.productID;
    //        $scope.ProductName = res.productName;
    //        $scope.OperType = 0;
    //    },
    //     function (errorPl) {
    //         console.log('Some Error in Getting Details', errorPl);
    //     });
    //}

    ////To Clear all Inputs controls value.  
    //$scope.clear = function () {
    //    $scope.OperType = 1;
    //    $scope.ProductID = "";
    //    $scope.ProductName = "";
    //}

    //$scope.addupdate = function (ProductNew) {
    //    //var promiseGetUpdate = CRUD_OperService.addupdate(ProductNew.productID);
    //    $location.path("/update/" + ProductNew.productID)
    //    //$location.path("/update?id=" + ProductNew.productID)
    //    $rootScope.prodid = ProductNew.productID;
    //}

});