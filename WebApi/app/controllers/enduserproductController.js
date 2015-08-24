//app.controller('productController',['$scope','CRUD_OperService','$log', function ($scope, CRUD_OperService,$log) {
app.controller('enduserproductController', function ($scope, enduserproductService,$log,$location,$rootScope) {
    $scope.OperType = 1;
  
    //1 Mean New Entry  

    GetAllRecords();
    //To Get All Records  
    function GetAllRecords() {
        var promiseGet = enduserproductService.getAllProduct();
        promiseGet.then(function (pl) {
            $scope.EndUser = pl.data
        },
              function (errorPl) {
                  $log.error('Some Error in Getting Records.', errorPl);
              });
    }
    $scope.addupdate = function (endUserDetails) {
        //var promiseGetUpdate = CRUD_OperService.addupdate(ProductNew.productID);
        $location.path("/userupdate/" + endUserDetails.productID)
        //$location.path("/update?id=" + ProductNew.productID)
        $rootScope.prodid = endUserDetails.productID;
    }

});