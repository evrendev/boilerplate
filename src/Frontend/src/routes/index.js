import Vue from "vue"
import VueRouter from "vue-router"
import Home from "../views/app/Home.vue"
import AppLayout from "../layouts/App.vue"
import AdminLayout from "../layouts/Admin.vue"
import EmptyLayout from "../layouts/Empty.vue"

Vue.use(VueRouter)

const routes = [
  {
    path: "/",
    name: "Home",
    component: Home,
    meta: {
      layout: AppLayout
    }
  },
  {
    path: "/login",
    name: "Login",
    component: () => import("../views/admin/Login.vue"),
    meta: {
      layout: EmptyLayout
    }
  },
  {
    path: "/admin/",
    name: "admin.dashboard",
    component: () => import("../views/admin/Dashboard.vue"),
    meta: {
      layout: AdminLayout
    }
  }
]

const router = new VueRouter({
  mode: "history",
  base: process.env.BASE_URL,
  routes
})

export default router
