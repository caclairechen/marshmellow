<template>
  <v-app>
    <div data-app id="app">
      <Navigation />
      <span>Home</span>
    </div>
  </v-app>
</template>

<script>
import Navigation from "@/components/Navigation.vue";

export default {
  components: {
    Navigation,
  },

  data: () => ({
    //
  }),
};
</script>
