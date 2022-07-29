import axiosInstance from "../config";

//const responseBody =  (response) => response?.data;

export const requestAPI = {
  get: async (url) => await axiosInstance.get(url),
  post: (url, body) => axiosInstance.post(url, body),
  put: (url, body) => axiosInstance.put(url, body),
  patch: async (url, body) => await axiosInstance.patch(url, body),
  delete: (url) => axiosInstance.delete(url),
};
