//app.controller('productController',['$scope','CRUD_OperService','$log', function ($scope, CRUD_OperService,$log) {
app.controller('mediaController',['$scope','mediaService','$routeParams', function ($scope, mediaService, $routeParams,$log) {
    //1 Mean New Entry  
    $scope.OperType = 1;

    $scope.UpdateID = $routeParams.updateID;

    var id = $scope.UpdateID;
   
    GetAllRecords();
    //To Get All Records  
    function GetAllRecords() {
        var promiseGet = mediaService.getAllMedia(id);
        promiseGet.then(function (pl) {

            $scope.Media = pl.data;
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
        $scope.MediaName = "";
    }

    //To Clear all Inputs controls value.  
    $scope.clear = function () {
        $scope.OperType = 1;
        $scope.MediaName = "";
      }


    //To Delete Media  
    $scope.delete = function (Media) {
        var promiseDelete = mediaService.delete(Media.mediaID);
        promiseDelete.then(function (pl) {
            $scope.Message = "Media Deleted Successfuly";
            GetAllRecords();
            ClearModels();
        }, function (err) {
            console.log("Err" + err);
        });

    }

    //To Create new record and Edit an existing Record.  
    $scope.save = function () {
        var Media = {
            MediaName: $scope.MediaName
        };
        if ($scope.OperType === 1) {
            var promisePost = mediaService.post(id, Media);
            promisePost.then(function (pl) {
                $scope.MediaID = pl.data.MediaID;
                $scope.Message = "Media Added Successfuly";
                GetAllRecords();
               ClearModels();
            }, function (err) {
                console.log("Err" + err);
            });
        } else {
            //Edit the record                
            Media.MediaID = $scope.MediaID;
            var promisePut = mediaService.put($scope.MediaID, Media);
            promisePut.then(function (pl) {
                $scope.Message = "Media edited Successfuly";
                GetAllRecords();
                ClearModels();
            }, function (err) {
                console.log("Err" + err);
            });
        }
    };


    //Upload files.
//    private uploadFile($files) {
//        var fileName = $files[0].name;
//        var file = $files;
        
//        this.$upload.upload({
//            //Call api to upload.
//            url: apiPaths.UploadDocumentToAzure,
//            file: file
//        })
//    }).progress(function (evt) {
//        // get upload percentage
//        console.log('percent: ' + parseInt(100.0 * evt.loaded / evt.total));
//    }).success(function (data, status, headers, config) {
//        // file is uploaded successfully
//        console.log(data);
//    }).error(function (data, status, headers, config) {
//        // file failed to upload
//        console.log(data);
//    });
//})

$scope.uploadFile = function($files){
    var fileName = $files[0].name;
    var file = $files;
        
    this.$upload.upload({
        //Call api to upload.
        url: apiPaths.UploadDocumentToAzure,
        file: file
        //})
    }).progress(function (evt) {
        // get upload percentage
        console.log('percent: ' + parseInt(100.0 * evt.loaded / evt.total));
    }).success(function (data, status, headers, config) {
        // file is uploaded successfully
        console.log(data);
    }).error(function (data, status, headers, config) {
        // file failed to upload
        console.log(data);
    });
    //});
};
}]);