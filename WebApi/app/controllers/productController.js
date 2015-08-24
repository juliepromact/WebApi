//app.controller('productController',['$scope','CRUD_OperService','$log', function ($scope, CRUD_OperService,$log) {
app.controller('productController', function ($scope, CRUD_OperService, $location,$rootScope,$log) {
    $scope.OperType = 1;  
    //1 Mean New Entry  
  
    GetAllRecords();  
    //To Get All Records  
    function GetAllRecords() {  
        var promiseGet = CRUD_OperService.getAllProduct();  
        promiseGet.then(function (pl) {
            $scope.ProductNew = pl.data
        },
              function (errorPl) {  
                    $log.error('Some Error in Getting Records.', errorPl);  
              });  
    }  
  
    //To Clear all input controls.  
    function ClearModels() {  
        $scope.OperType = 1;  
        $scope.ProductID = "";
        $scope.ProductName = "";         
    }   
    
    //To Create new record and Edit an existing Record.  
    $scope.save = function () {  
        var ProductNew = {
            ProductName: $scope.ProductName
        };  
        if ($scope.OperType === 1) {  
            var promisePost = CRUD_OperService.post(ProductNew);
            promisePost.then(function (pl) {  
                $scope.ProductID = pl.data.ProductID;
                $scope.Message = "Product Added Successfuly";
                GetAllRecords();  
                ClearModels();  
            }, function (err) {  
                console.log("Err" + err);  
            });  
        } else {  
            //Edit the record                
            ProductNew.ProductID = $scope.ProductID;
            var promisePut = CRUD_OperService.put($scope.ProductID, ProductNew);
            promisePut.then(function (pl) {  
                $scope.Message = "Product Updated Successfuly";
                GetAllRecords();  
                ClearModels();  
            }, function (err) {  
                console.log("Err" + err);  
            });   
        }  
    };  
  
    //To Delete Record  
    $scope.delete = function (ProductNew) {
        var promiseDelete = CRUD_OperService.delete(ProductNew.productID);
        promiseDelete.then(function (pl) {
            $scope.Message = "Product Deleted Successfuly";
            GetAllRecords();  
            ClearModels();
            //     $location.path('/login?id=' + ProductNew.productID);
        }, function (err) {  
            console.log("Err" + err);  
        });  
    }
  
    //To Get Student Detail on the Base of Student ID  
    $scope.get = function (ProductNew) {
        var promiseGetSingle = CRUD_OperService.get(ProductNew.productID);
        promiseGetSingle.then(function (pl) {  
            var res = pl.data;
            $scope.ProductID = res.productID;
            $scope.ProductName = res.productName;  
            $scope.OperType = 0;  
        },   
         function (errorPl) {
             console.log('Some Error in Getting Details', errorPl);
         });
    }

    //To Clear all Inputs controls value.  
    $scope.clear = function () {
        $scope.OperType = 1;
        $scope.ProductID = "";
        $scope.ProductName = "";      
    }

    $scope.addupdate = function (ProductNew) {
        //var promiseGetUpdate = CRUD_OperService.addupdate(ProductNew.productID);
        $location.path("/update/" + ProductNew.productID)
       //$location.path("/update?id=" + ProductNew.productID)
        $rootScope.prodid = ProductNew.productID;
    }
    
});