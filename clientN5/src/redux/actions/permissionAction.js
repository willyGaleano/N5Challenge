import { message } from "antd";
import { permissionsAPI } from "../../services/api/permissionServiceAPI"
import { types } from "../types/types";

export const listAllPermissions = (req) => {
    return async (dispatch) => {
        try
        {
            const resp = await permissionsAPI.getAllPermissions(req);
            dispatch(syncListAll(resp))
        }
        catch(ex)
        {
            dispatch(syncListAll([]))
            message.error(`Error: ${ex.message}`, 5);
        }
    }
}

const syncListAll = (resp) => ({
  type: types.listAllPermissions,
  payload: resp,
});