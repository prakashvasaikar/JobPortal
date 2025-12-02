app.factory("careerFactory", function ($http) {
    var baseUrl = "/api/API/";

    return {
        getJobRequirementList: () =>
            $http.get(baseUrl + "getJobRequirementActiveList"),

        saveApplyJob: objCareer => {
            debugger
            var formData = new FormData();
            formData.append("PrimarySkill", objCareer.primarySkill);
            formData.append("ExperienceYear", objCareer.experienceYear);
            formData.append("ExperienceMonth", objCareer.experienceMonth);
            formData.append("ReferBy", objCareer.referBy);
            formData.append("Id", objCareer.id);
            formData.append("Status", objCareer.status);
            formData.append("RefId_CompanyRequirement", objCareer.refId_CompanyRequirement || 0);
            formData.append("RefId_UserMaster", objCareer.refId_UserMaster || 0);
            if (objCareer.resume) {
                formData.append("resume", objCareer.resume);
            }
            return $http.post(baseUrl + "saveApplyJob", formData, {
                transformRequest: angular.identity,
                headers: { "Content-Type": undefined }
            });
        }

    };
});
