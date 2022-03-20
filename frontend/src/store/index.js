import Vue from "vue";
import Vuex from "vuex";
import axios from "axios";

Vue.use(Vuex);

export default new Vuex.Store({
  state: {
    users: [],
  },
  getters: {},
  mutations: {
    SET_USERS(state, val) {
      state.users = val;
    },
  },
  actions: {
    async loadUsers({ commit }) {
      try {
        const response = await axios.get("https://localhost:7124/Users");
        commit("SET_USERS", response.data);
      } catch (error) {
        console.log(error);
      }
    },
  },
});
