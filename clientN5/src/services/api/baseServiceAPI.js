import axiosInstance from "../config";

const responseBody =  (response) => response?.data;

export const requestAPI = {
  get: (url) => axiosInstance.get(url).then(responseBody),
  post: (url, body) => axiosInstance.post(url, body).then(responseBody),
  put: (url, body) => axiosInstance.put(url, body).then(responseBody),
  patch: (url, body) => axiosInstance.patch(url, body).then(responseBody),
  delete: (url) => axiosInstance.delete(url).then(responseBody),
};
