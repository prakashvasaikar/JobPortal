app.factory("registerFactory", function ($http) {
    var baseUrl = "/api/API/";

    return {
        register: objRegister => $http.post(baseUrl + "registration", objRegister),
        countryGetlist: () => $http.get(baseUrl + "getCountryList"),
        stateGetlist: id => $http.get(baseUrl + "getStateListByCountryId/" + id),
        cityGetlist: id => $http.get(baseUrl + "getCityListByStateId/" + id),
    };
});
