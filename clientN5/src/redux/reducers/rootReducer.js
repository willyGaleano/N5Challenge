import { combineReducers } from "redux";
import { permissionReducer } from "./permissionReducer";

export const rootReducer = combineReducers({
  permission: permissionReducer
});