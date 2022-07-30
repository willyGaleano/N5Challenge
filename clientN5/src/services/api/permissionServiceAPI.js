import { requestAPI } from "./baseServiceAPI";

export const permissionsAPI = {
  getAllPermissions: async (req) => 
    await requestAPI.get(`/Challenge/GetPermissions?NombreEmpleado=${req.nombreEmpleado}&PageNumber=${req.pageNumber}&PageSize=${req.pageSize}`),
  
  changedPermission: async (req) => await requestAPI.patch(`/Challenge/RequestPermission/${req.permisoId}`),

  updatePermission: async (permissionId, body) => await requestAPI.put(`/Challenge/ModifyPermission/${permissionId}`, body)
};