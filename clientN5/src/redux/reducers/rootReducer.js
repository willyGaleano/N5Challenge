import { combineReducers } from "redux";
import { permissionTypesReducer } from "./permissionTypesReducer";

export const rootReducer = combineReducers({
  permissionTypes: permissionTypesReducer
});