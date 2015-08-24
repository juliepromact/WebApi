//app.controller('productController',['$scope','CRUD_OperService','$log', function ($scope, CRUD_OperService,$log) {
app.controller('enduserupdateController', function ($scope, enduserupdateService, $location, $routeParams,$rootScope,$log) {
    $scope.OperType = 1;
    $scope.ProductID = $routeParams.productID;
    var id = $scope.ProductID;
    //1 Mean New Entry  
    GetAllRecords();
    //To Get All Records  
    function GetAllRecords() {
        var promiseGet = enduserupdateService.getAllUpdate(id);
        promiseGet.then(function (pl) {

            $scope.Update = pl.data;
        },
              function (errorPl) {
                  $log.error('Some Error in Getting Records.', errorPl);

              });
    }

    

    $scope.addmedia = function (Update) {
        $location.path("/usermedia/" + Update.updateID)
        $rootScope.upid = Update.updateID;
    }

});