import Vue from "vue";
import Vuex from "vuex";
import axios from "axios";

Vue.use(Vuex);

export default new Vuex.Store({
  state: {
    users: [],
    pets: [],
  },
  getters: {},
  mutations: {
    SET_USERS(state, val) {
      state.users = val;
    },
    ADD_PET(state, val) {
      state.pets.add(val);
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
    async addPet({ commit }, pet) {
      try {
        await axios.post("https://localhost:7124/users/1/pets", pet);
        commit("ADD_PET", pet);
      } catch (error) {
        console.log(error);
      }
    },
  },
});
