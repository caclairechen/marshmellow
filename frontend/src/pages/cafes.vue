<template>
  <v-app>
    <div data-app id="app">
      <Navigation />
      <span>My Cafes</span>
    </div>
  </v-app>
</template>

<script>
import Navigation from "@/components/Navigation.vue";

export default {
  components: {
    Navigation,
  },
};
</script>
