export default [
  {
    label: "Yönetim Paneli",
    childs: [
      {
        label: "Kontrol Paneli",
        name: "admin.dashboard",
        to: "/admin",
        icon: "fas fa-tachometer-alt"
      },
      {
        label: "İçerik Yönetimi",
        name: "admin.contents",
        to: "/admin/contents",
        icon: "far fa-copy"
      }
    ]
  },
  {
    label: "Ayarlar",
    childs: [
      {
        label: "Departman Yönetimi",
        name: "admin.settings.departments",
        to: "/admin/settings/departments",
        icon: "far fa-building"
      },
      {
        label: "Kullanıcı Yönetimi",
        name: "admin.settings.users",
        to: "/admin/settings/users",
        icon: "fas fa-users"
      }
    ]
  }
]
