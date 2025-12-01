app.controller('MainCtrl', function ($scope, toastr) {

    $scope.showSuccess = function () {
        toastr.success('Data saved successfully!', 'Success');
    };

    $scope.showError = function () {
        toastr.error('Something went wrong!', 'Error');
    };

    $scope.showInfo = function () {
        toastr.info('This is an info message.', 'Info');
    };

    $scope.showWarning = function () {
        toastr.warning('This is a warning.', 'Warning');
    };
});
