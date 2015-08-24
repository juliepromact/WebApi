//app.controller('productController',['$scope','CRUD_OperService','$log', function ($scope, CRUD_OperService,$log) {
app.controller('updateController', function ($scope, updateService, $location,$routeParams) {
    $scope.OperType = 1;
    $scope.ProductID = $routeParams.productID;
    var id = $scope.ProductID;
    //1 Mean New Entry  
    GetAllRecords();
    //To Get All Records  
    function GetAllRecords() {
        var promiseGet = updateService.getAllUpdate(id);
        promiseGet.then(function (pl) {
          
            $scope.Update = pl.data;
        },
              function (errorPl) {
                    $log.error('Some Error in Getting Records.', errorPl);  

              });
    }

    ////To Get Update Detail on the Base of Update ID  
    //$scope.getone = function (Update) {
    //    var promiseGetSingle = updateService.get(Update.updateID);
    //    promiseGetSingle.then(function (pl) {
    //        var res = pl.data;
    //        $scope.UpdateID = res.updateID;
    //        $scope.UpdateIntro = res.updateIntro;
    //        $scope.UpdateDetail = res.updateDetail;
    //      //  $scope.Product_ID = res.product_ID;
    //        $scope.OperType = 0;
    //    },
    //     function (errorPl) {
    //         console.log('Some Error in Getting Details', errorPl);
    //     });
    //}

    //To Clear all input controls.  
    function ClearModels() {
        $scope.OperType = 1;
        $scope.UpdateID = "";
        $scope.UpdateIntro = "";
        $scope.UpdateDetail = "";

    }

    //To Clear all Inputs controls value.  
    $scope.clear = function () {
        $scope.OperType = 1;
        $scope.UpdateID = "";
        $scope.UpdateIntro = "";
        $scope.UpdateDetail = "";
       
    }


    //To Delete Update  
    $scope.delete = function (Update) {
        var promiseDelete = updateService.delete(Update.updateID);
        promiseDelete.then(function (pl) {
            $scope.Message = "Update Deleted Successfuly";
            GetAllRecords();
             ClearModels();
            //     $location.path('/login?id=' + ProductNew.productID);
        }, function (err) {
            console.log("Err" + err);
        });

    }
    //To Create new record and Edit an existing Record.  
    $scope.save = function () {
        var Update = {
            UpdateIntro: $scope.UpdateIntro,
            UpdateDetail: $scope.UpdateDetail
        };
        if ($scope.OperType === 1) {
            var promisePost = updateService.post(id,Update);
            promisePost.then(function (pl) {
                $scope.UpdateID = pl.data.UpdateID;
                $scope.Message = "Update Added Successfuly";
                GetAllRecords();
                ClearModels();
            }, function (err) {
                console.log("Err" + err);
            });
        } else {
            //Edit the record                
            Update.UpdateID = $scope.UpdateID;
            var promisePut = updateService.put($scope.UpdateID, Update);
            promisePut.then(function (pl) {
                $scope.Message = "Update edited Successfuly";
                GetAllRecords();
                ClearModels();
            }, function (err) {
                console.log("Err" + err);
            });
        }
    };


    $scope.addmedia = function (Update) {
        $location.path("/media/" + Update.updateID)
        $rootScope.upid = Update.updateID;
    }

    $scope.checkIntro = function () {
       
        if ($scope.UpdateIntro.length > 14) {
            $scope.Message = "Introduction must be less than 14 characters.";
            return false;
        }
        else
            $scope.Message = "";
    }

    $scope.checkDetail = function()
    {
        var str=$scope.UpdateDetail;
        var words = str.split(/\b[\s,\.-:;]*/);
        var len = words.length;
        if (len > 0) {
            $scope.words = "Number of words : " + len;
        }
        else {
            $scope.words = "";
        }
        if (len > 5) {
            $scope.Message = "kam likho";
            $scope.UpdateDetail=(UpdateDetail.value.slice(0, -1));
            alert("You've reached the maximum allowed words.");
            
        }
        else
            $scope.Message = "";
      
    }

});