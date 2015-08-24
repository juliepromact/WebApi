//app.controller('productController',['$scope','CRUD_OperService','$log', function ($scope, CRUD_OperService,$log) {
app.controller('signuprequestController', function ($scope, signuprequestService, $location, $rootScope) {
    $scope.OperType = 1;
    //1 Mean New Entry  

    GetAllRecords();
    //To Get All Records  
    function GetAllRecords() {
        var promiseGet = signuprequestService.getAllSignupRequest();
        promiseGet.then(function (pl) { $scope.ProductOwner = pl.data },
              function (errorPl) {
                  //  $log.error('Some Error in Getting Records.', errorPl);  

              });
    }

    //To Delete Record  
    $scope.delete = function (ProductOwner) {
        var promiseDelete = signuprequestService.delete(ProductOwner.id);
        promiseDelete.then(function (pl) {
            $scope.Message = "Signup Request Deleted Successfuly";
            GetAllRecords();
            //ClearModels();
            //     $location.path('/login?id=' + ProductNew.productID);
        }, function (err) {
            console.log("Err" + err);
        });
    }


    //To Clear all input controls.  
    function ClearModels() {
        $scope.OperType = 1;
        $scope.ID = "";
        $scope.OwnerName = "";
        $scope.CompanyName = "";
        $scope.Email = "";
        $scope.Message = "";
    }

    //To Create new record and Edit an existing Record.  
    $scope.request = function () {
        var ProductOwner = {
            OwnerName: $scope.OwnerName,
            CompanyName: $scope.CompanyName,
            Email: $scope.Email
        };
        if ($scope.OperType === 1) {
            var promisePost = signuprequestService.post(ProductOwner);
            promisePost.then(function (pl) {
                $scope.ID = pl.data.ID;
                ClearModels();
                $scope.Message = "Request Submitted Successfully";
               // GetAllRecords();

            }, function (err) {
                ClearModels();
                $scope.Message = "Request Submission Failed Successfully (lol)";
                console.log("Err" + err);
            });
        } else {
            //Edit the record                
            ProductOwner.ID = $scope.ID;
            var promisePut = signuprequestService.put($scope.ID, ProductOwner);
            promisePut.then(function (pl) {
                $scope.Message = "Product Updated Successfuly";
                GetAllRecords();
                //ClearModels();
            }, function (err) {
                console.log("Err" + err);
            });
        }
    };

  
    //To Get Student Detail on the Base of Student ID  
    $scope.get = function (ProductOwner) {
        var promiseGetSingle = signuprequestService.get(ProductOwner.id);
        promiseGetSingle.then(function (pl) {
            var res = pl.data;
            $scope.Message = "Invitation Send Successfuly";
            },
         function (errorPl) {
             console.log('Some Error in Getting Details', errorPl);
         });
    }

  
    $scope.approve = function (ProductOwner) {
        var promiseApprove = signuprequestService.put($scope.ID, ProductOwner);
        promiseApprove.then(function (pl) {
            $scope.Message = "Invitation Send Successfuly";
            GetAllRecords();
            //ClearModels();
            //     $location.path('/login?id=' + ProductNew.productID);
        }, function (err) {
            console.log("Err" + err);
        });
    }

});