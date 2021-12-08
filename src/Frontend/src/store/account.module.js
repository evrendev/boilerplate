import axios from "axios"

const token = localStorage.getItem("token")
const initialState = token
  ? {
      token,
      loggedIn: true
    }
  : {
      token: "",
      loggedIn: false
    }

const actions = {
  LOGIN: "login",
  LOGOUT: "logout",
  INIT: "init",
  FETCH_TOKEN: "fetchToken"
}

const mutations = {
  SET_TOKEN: "setToken"
}

export const account = {
  namespaced: true,
  state: () => ({
    loggedIn: {
      type: Boolean,
      default: false
    }
  }),
  actions: {
    async [actions.INIT]({ dispatch }) {
      await dispatch(actions.FETCH_TOKEN)
    },
    async [actions.LOGIN]({ commit }, credentials) {
      const response = await axios.post("/auth/login", credentials, {
        headers: {}
      })

      let data = {
        loggedIn: false,
        data: null
      }

      if (response.status == 200 && !response.data.error) {
        data = {
          loggedIn: true,
          data: response.data.data
        }

        commit(mutations.SET_TOKEN, data)
      }

      return response.data
    },
    async [actions.LOGOUT]({ commit }) {
      let data = {
        loggedIn: false,
        data: null
      }

      commit(mutations.SET_TOKEN, data)
    },
    async [actions.FETCH_TOKEN]({ commit }) {
      commit(mutations.SET_TOKEN, initialState)
    }
  },
  mutations: {
    [mutations.SET_TOKEN](state, data) {
      localStorage.setItem("data", JSON.stringify(data))
      state.loggedIn = data.loggedIn
    }
  }
}
