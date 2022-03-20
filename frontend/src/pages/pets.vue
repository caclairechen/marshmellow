<template>
  <v-app>
    <div data-app id="app">
      <Navigation />
      <span>My Pets</span>
      <div v-for="pet in pets" v-bind:key="pet.id">
        <h2>{{ pet.name }}</h2>
      </div>
      <AddPet />
    </div>
  </v-app>
</template>

<script>
import Navigation from "@/components/Navigation.vue";
import AddPet from "@/components/AddPet.vue";

export default {
  components: {
    Navigation,
    AddPet,
  },
  computed: {
    pets() {
      return this.$store.state.pets;
    },
  },
  created() {
    this.$store.dispatch("loadPets");
  },
};
</script>
