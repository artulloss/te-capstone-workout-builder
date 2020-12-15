import Vue from "vue";
import Router from "vue-router";
import Home from "@/views/Home.vue";
import Login from "@/views/Login.vue";
import Logout from "@/views/Logout.vue";
import Exercises from "@/views/Exercises.vue";
import AdminExercises from "@/views/AdminExercises.vue";
import Register from "@/views/Register.vue";
import store from "@/store/index";
import Landing from "@/views/Landing.vue";
import CreateExercise from "@/views/CreateExercise.vue";
import GenerateWorkout from "@/views/GenerateWorkout.vue";
import WorkoutHistory from "@/views/WorkoutHistory.vue";

Vue.use(Router);

/**
 * The Vue Router is used to "direct" the browser to render a specific view component
 * inside of App.vue depending on the URL.
 *
 * It also is used to detect whether or not a route requires the user to have first authenticated.
 * If the user has not yet authenticated (and needs to) they are redirected to /login
 * If they have (or don't need to) they're allowed to go about their way.
 */

const router = new Router({
  mode: "history",
  base: process.env.BASE_URL,
  routes: [
    {
      path: "/",
      name: "landing",
      component: Landing,
      meta: {
        requiresAuth: false,
      },
    },
    {
      path: "/home",
      name: "home",
      component: Home,
      meta: {
        requiresAuth: true,
      },
    },
    {
      path: "/login",
      name: "login",
      component: Login,
      meta: {
        requiresAuth: false,
      },
    },
    {
      path: "/logout",
      name: "logout",
      component: Logout,
      meta: {
        requiresAuth: false,
      },
    },
    {
      path: "/register",
      name: "register",
      component: Register,
      meta: {
        requiresAuth: false,
      },
    },
    {
      path: "/admin-exercises",
      name: "admin-exercises",
      component: AdminExercises,
      meta: {
        requiresAuth: true,
        admin: true,
      },
    },
    {
      path: "/exercises",
      name: "exercises",
      component: Exercises,
      meta: {
        requiresAuth: true,
      },
    },
    {
      path: "/create-exercise",
      name: "create-exercise",
      component: CreateExercise,
      meta: {
        requiresAuth: true,
      },
    },
    {
      path: "/generate-workout",
      name: "generate-workout",
      component: GenerateWorkout,
      meta: {
        requiresAuth: true,
      },
    },
    {
      path: "/workout-history",
      name: "workout-history",
      component: WorkoutHistory,
      meta: {
        requiresAuth: true,
      },
    },
  ],
});

router.beforeEach((to, from, next) => {
  // Determine if the route requires Authentication
  const requiresAuth = to.matched.some((x) => x.meta.requiresAuth);
  console.log(to);
  const admin = to.matched.some((x) => x.meta["admin"]);
  // If it does and they are not logged in, send the user to "/login"
  if (requiresAuth && store.state.token === "") {
    next("/login");
  } else if (admin && store.state.user.role !== "admin") {
    next(false);
  } else {
    // Else let them go to their next destination
    next();
  }
});

export default router;
