//app.controller('productController',['$scope','CRUD_OperService','$log', function ($scope, CRUD_OperService,$log) {
app.controller('endusermediaController', ['$scope', 'endusermediaService', '$routeParams', function ($scope, endusermediaService, $routeParams,$log) {
    //1 Mean New Entry  
    $scope.OperType = 1;

    $scope.UpdateID = $routeParams.updateID;

    var id = $scope.UpdateID;

    GetAllRecords();
    //To Get All Records  
    function GetAllRecords() {
        var promiseGet = endusermediaService.getAllMedia(id);
        promiseGet.then(function (pl) {

            $scope.Media = pl.data;
        },
              function (errorPl) {
                  $log.error('Some Error in Getting Records.', errorPl);

              });
    }

}]);