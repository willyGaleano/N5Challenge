import { types } from "../types/types";
const initialState = [];

export const permissionReducer = (state = initialState, action) => {
  switch (action.type) {
    case types.listPermissions:
      return [...state];
      
    default:
      return state;
  }
};