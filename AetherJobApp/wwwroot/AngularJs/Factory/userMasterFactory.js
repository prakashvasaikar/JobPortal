app.factory("userFactory", function ($http) {
    var baseUrl = "/api/API/";

    return {
        getAll: () => $http.get(baseUrl + "getUserList"),
        login: objLogin => $http.post(baseUrl + "login", objLogin),
        register: objRegister => $http.post(baseUrl + "registration", objRegister)
    };
});
