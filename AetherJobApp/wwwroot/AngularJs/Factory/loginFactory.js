app.factory("userFactory", function ($http) {
    var baseUrl = "/api/API/";

    return {
        login: objLogin => $http.post(baseUrl + "login", objLogin)
    };
});
