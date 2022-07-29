import { requestAPI } from "./baseServiceAPI";

export const permissionsAPI = {
  getAllPermissions: async (req) => await requestAPI.get(`/Challenge/GetPermissions?NombreEmpleado=${req.nombreEmpleado}`),
  
  changedPermission: async (req) => await requestAPI.patch(`/Challenge/RequestPermission/${req.permisoId}`, req),

  updatePermission: (req) => 
    requestAPI.put(`/Challenge/GetPermissions?NombreEmpleado=${req.nombreEmpleado}`, req)  
};