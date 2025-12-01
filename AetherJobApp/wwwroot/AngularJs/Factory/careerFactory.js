app.factory("careerFactory", function ($http) {
    var baseUrl = "/api/API/";

    return {
        getJobRequirementList: () => $http.get(baseUrl + "getJobRequirementList")
    };
});
