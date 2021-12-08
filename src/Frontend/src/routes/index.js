import Vue from "vue"
import Router from "vue-router"
import Login from "../views/account/Login"
import Home from "../views/app/Home"
import Dashboard from "../views/admin/Dashboard"
import AdminLayout from "../layouts/Admin"
import AccountLayout from "../layouts/Account"
import AppLayout from "../layouts/App"
import EmptyLayout from "../layouts/Empty"

Vue.use(Router)

export default function init(store) {
  return new Router({
    mode: "history",
    base: process.env.BASE_URL,
    routes: [
      {
        path: "/",
        name: "app.home",
        component: Home,
        meta: {
          layout: AppLayout
        }
      },
      {
        path: "/account/login",
        component: Login,
        meta: {
          layout: AccountLayout
        },
        beforeEnter(to, from, next) {
          if (store.state.account.loggedIn) return next("/")
          return next()
        }
      },
      {
        path: "/account/forgot-password",
        name: "Login",
        component: () => import("../views/account/ForgotPassword.vue"),
        meta: {
          layout: AccountLayout
        }
      },
      {
        path: "/admin",
        name: "admin.dashboard",
        component: Dashboard,
        meta: {
          layout: AdminLayout
        },
        beforeEnter(to, from, next) {
          if (store.state.account.loggedIn) return next()
          return next("/account/login")
        }
      },
      {
        path: "/admin/contents",
        name: "admin.contents",
        component: () => import("../views/admin/Contents.vue"),
        meta: {
          layout: AdminLayout
        },
        beforeEnter(to, from, next) {
          if (store.state.account.loggedIn) return next()
          return next("/account/login")
        }
      },
      {
        path: "/admin/settings/departments",
        name: "admin.settings.departments",
        component: () => import("../views/admin/Departments.vue"),
        meta: {
          layout: AdminLayout
        },
        beforeEnter(to, from, next) {
          if (store.state.account.loggedIn) return next()
          return next("/account/login")
        }
      },
      {
        path: "/admin/settings/users",
        name: "admin.settings.users",
        component: () => import("../views/admin/Users.vue"),
        meta: {
          layout: AdminLayout
        },
        beforeEnter(to, from, next) {
          if (store.state.account.loggedIn) return next()
          return next("/account/login")
        }
      },
      {
        path: "*",
        name: "notFound",
        component: () => import("../views/app/PageNotFound.vue"),
        meta: {
          layout: EmptyLayout
        }
      }
    ]
  })
}
