app.factory("registerFactory", function ($http) {
    var baseUrl = "/api/API/";

    return {
        register: objRegister => $http.post(baseUrl + "registration", objRegister)
    };
});
