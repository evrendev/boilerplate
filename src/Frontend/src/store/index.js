import Vue from "vue"
import Vuex from "vuex"
import axios from "axios"

import { account } from "./account.module"
import { dashboard } from "./dashboard.module"

Vue.use(Vuex)

axios.defaults.baseURL = process.env.VUE_APP_BACKEND_URL

const store = new Vuex.Store({
  modules: {
    account,
    dashboard
  }
})

export default async function init() {
  await store.dispatch("account/init")
  await store.dispatch("dashboard/init")

  return store
}
