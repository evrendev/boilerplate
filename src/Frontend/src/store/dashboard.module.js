const actions = {
  INIT: "init",
  SET_DEFAULTS: "setDefaults",
  TOGGLE_MENU: "toggleMenu"
}

const mutations = {
  SET_LOADING: "setLoading",
  SET_DEFAULTS: "setDefaults",
  TOGGLE_MENU: "toggleMenu"
}

export const dashboard = {
  namespaced: true,
  state: () => ({
    loading: {
      type: Boolean,
      default: true
    },
    menuOpened: {
      type: Boolean,
      default: false
    },
    languages: {
      type: Array,
      default: []
    }
  }),
  actions: {
    async [actions.INIT]({ dispatch }) {
      await dispatch(actions.SET_DEFAULTS)
    },
    [actions.SET_DEFAULTS]({ commit }) {
      commit(mutations.SET_DEFAULTS)
    },
    [actions.TOGGLE_MENU]({ commit }) {
      commit(mutations.TOGGLE_MENU)
    }
  },
  mutations: {
    [mutations.SET_LOADING](state, isLoading) {
      //Set Loading
      state.loading = isLoading
    },
    [mutations.SET_DEFAULTS](state) {
      //Set Defaults Dashboard
      state.loading = false
      state.menuOpened = false
    },
    [mutations.TOGGLE_MENU](state) {
      //Set Mobile Menu
      state.menuOpened = !state.menuOpened
    }
  }
}
