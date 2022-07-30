import { message } from "antd";
import { permissionTypesAPI } from "../../services/api/permissionTypesServicesAPI";
import { types } from "../types/types";

export const listAllPermissionTypes = () => {
    return async (dispatch) => {
        try
        {
            const resp = await permissionTypesAPI.getAllPermissionTypes();            
            console.log(resp.data);
            dispatch(syncListAll(resp.data))
        }
        catch(ex)
        {
            dispatch(syncListAll([]))
            message.error(`Error: ${ex.message}`, 5);
        }
    }
}

const syncListAll = (resp) => ({
  type: types.listPermissionTypes,
  payload: resp,
});