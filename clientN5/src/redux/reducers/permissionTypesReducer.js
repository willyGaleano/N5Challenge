import { types } from "../types/types";
const initialState = [];

export const permissionTypesReducer = (state = initialState, action) => {
  switch (action.type) {
    case types.listPermissionTypes:
      return [...action.payload];
    default:
      return state;
  }
};