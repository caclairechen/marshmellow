<template>
  <v-app>
    <div data-app id="app">
      <Navigation />
      <span>My Pets</span>
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
};
</script>
