<template>
  <div>
    <h1>This is an storage page</h1>
  </div>
  <div> 
    
  </div>
</template>

<script>
export default {
  
data(){
  return {
    storages: [],
    count: 3};
},
async mounted(){
  const {data} = await this.axios.get(variables.API_URL+"storage");
  this.storages = data;
}
}
</script>
