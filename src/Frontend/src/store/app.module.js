import i18n, { selectedLocale } from "@/locales/i18n"

const actions = {
  INIT: "init",
  SET_DEFAULTS: "setDefaults",
  CHANGE_LOCALE: "changeLocale"
}

const mutations = {
  SET_DEFAULTS: "setDefaults",
  SET_LOCALE: "setLocale"
}

export const app = {
  namespaced: true,
  state: () => ({
    locale: selectedLocale
  }),
  actions: {
    async [actions.INIT]({ dispatch }) {
      await dispatch(actions.SET_DEFAULTS)
    },
    [actions.SET_DEFAULTS]({ commit }) {
      commit(mutations.SET_DEFAULTS)
    },
    [actions.CHANGE_LOCALE]({ commit }, newLocale) {
      commit(mutations.SET_LOCALE, newLocale)
    }
  },
  mutations: {
    [mutations.SET_DEFAULTS](state) {
      i18n.locale = selectedLocale
      state.locale = selectedLocale
    },
    [mutations.SET_LOCALE](state, newLocale) {
      console.log(newLocale)
      i18n.locale = newLocale
      state.locale = newLocale
    }
  }
}
