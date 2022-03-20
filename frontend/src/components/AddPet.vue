<template>
  <v-form ref="form" v-model="valid" lazy-validation>
    <v-text-field
      v-model="name"
      :counter="20"
      :rules="nameRules"
      label="Name"
      required
    ></v-text-field>

    <v-text-field
      v-model="age"
      :rules="ageRules"
      label="Age (years)"
      required
    ></v-text-field>

    <v-select
      v-model="gender"
      :items="genders"
      :rules="[(v) => !!v || 'Gender is required']"
      label="Gender"
      required
    ></v-select>

    <v-text-field
      v-model="species"
      :counter="10"
      :rules="speciesRules"
      label="Species"
      required
    ></v-text-field>

    <!-- <v-checkbox
      v-model="checkbox"
      :rules="[(v) => !!v || 'You must agree to continue!']"
      label="Do you agree?"
      required
    ></v-checkbox> -->

    <v-btn :disabled="!valid" color="success" class="mr-4" @click="validate">
      Validate
    </v-btn>

    <v-btn color="error" class="mr-4" @click="reset"> Reset Form </v-btn>

    <v-btn color="warning" @click="submitForm"> Submit </v-btn>
  </v-form>
</template>
<script>
export default {
  data: () => ({
    valid: true,
    name: "",
    nameRules: [
      (v) => !!v || "Name is required",
      (v) => (v && v.length <= 20) || "Name must be less than 20 characters",
    ],
    age: "",
    ageRules: [
      (v) => !!v || "Age is required",
      (v) => !isNaN(parseFloat(v)) || "Age must be a number",
      (v) => (v >= 0 && v <= 20) || "Age has to be between 0 to 20",
    ],
    gender: null,
    genders: ["Female", "Male"],
    species: "",
    speciesRules: [
      (v) => !!v || "Species is required",
      (v) => (v && v.length <= 10) || "Species must be less than 10 characters",
    ],
    // checkbox: false,
  }),

  methods: {
    validate() {
      this.$refs.form.validate();
    },
    reset() {
      this.$refs.form.reset();
    },
    submitForm() {
      console.log(this.name, this.age, this.gender, this.species);
      this.$store.dispatch("addPet", {
        name: this.name,
        age: Number(this.age),
        gender: this.gender,
        species: this.species,
      });
    },
  },
};
</script>
