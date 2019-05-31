<template>
  <div class="about">
    <h1>Senators Page</h1>
    <button @click="callSecureApi">Call API</button>
    <table>
        <thead>
            <tr>
                <th>#</th>
                <th>Name</th>
                <th>Party</th>
                <th>District</th>
                <th>Phone Number</th>
                <th>Email Address</th>
            </tr>
        </thead>
        <tbody v-for="(sen,index) in senators" :key="index">
            <tr>
                <th>{{ sen.id }}</th>
                <td>{{ sen.name }}</td>
                <td>{{ sen.party }}</td>
                <td>{{ sen.district }}</td>
                <td>{{ sen.phoneNumber }}</td>
                <td>{{ sen.emailAddress }}</td>
            </tr>
        </tbody>
    </table>
  </div>
</template>

<script>
import axios from 'axios'

export default {
  data() {
    return {
      values: ["no data yet"],
      senators: []
    }
  },
  methods: {
    async callApi() {
      try {
        const response = await axios.get("https://localhost:44329/api/values");
        this.values = response.data;
      } catch (err) {
        this.values.push("Ooops!" + err);
      }
    },
    async callSecureApi() {
      try {
        const response = await axios.get("https://localhost:44329/api/senators");
        this.senators = response.data;
      } catch (err) {
        console.log('secure api call failed');
      }
    }
  }
}
</script>