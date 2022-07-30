import { requestAPI } from "./baseServiceAPI";

export const permissionTypesAPI = {
  getAllPermissionTypes: async () => await requestAPI.get("/Extra/GetAllPermissionTypes"),
};