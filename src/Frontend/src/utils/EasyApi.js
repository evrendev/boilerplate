import axios from "axios"
const token = localStorage.getItem("token")

const baseURL = process.env.VUE_APP_BASE_PATH

const instance = axios.create({
  baseURL,
  headers: {
    Authorization: `Bearer ${token}`,
    "Content-Type": "application/json; charset=UTF-8"
  },
  withCredentials: false,
  JSON: true
})

class EasyApi {
  async get(url) {
    const response = await instance
      .get(url)
      .then((result) => {
        return result.data
      })
      .catch((error) => {
        if (error.reponse != undefined) {
          return {
            status: error.response.status,
            data: error.response.data
          }
        } else {
          return {
            message: error
          }
        }
      })

    return response
  }

  async post(url, data) {
    const response = await instance
      .post(url, data)
      .then((result) => {
        return result.data
      })
      .catch((error) => {
        if (error.reponse != undefined) {
          return {
            status: error.response.status,
            data: error.response.data
          }
        } else {
          return {
            message: error
          }
        }
      })

    return response
  }

  async put(url, data) {
    const response = await instance
      .put(url, data)
      .then((result) => {
        return result.data
      })
      .catch((error) => {
        if (error.reponse != undefined) {
          return {
            status: error.response.status,
            data: error.response.data
          }
        } else {
          return {
            message: error
          }
        }
      })

    return response
  }

  async delete(url) {
    const response = await instance
      .delete(url)
      .then((result) => {
        return result.data
      })
      .catch((error) => {
        if (error.reponse != undefined) {
          return {
            status: error.response.status,
            data: error.response.data
          }
        } else {
          return {
            message: error
          }
        }
      })

    return response
  }
}

export default new EasyApi()
