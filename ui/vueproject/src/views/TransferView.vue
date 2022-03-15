<template>
  <div>
    <div>
    <button type="button" class="btn btn-outline-secondary m-2" data-bs-toggle="modal" data-bs-target="#exampleModal"
            data-backdrop="static" data-keyboard="false">
      Добавить новое перемещение
    </button>
    </div>
    <div>
      <table class="table table-bordered align-middle">
        <thead>
          <tr>
            <th>Откуда</th>
            <th>Куда</th>
            <th>Время отбытия</th>
            <th>Время прибытия</th>
            <th>Список товаров</th>
            <th>Функция</th>
          </tr>
        </thead>
      <tbody>
        <tr v-for="transfer in TransferList" :key="transfer.id">
          <td class="text-left">{{transfer.From}}</td>
          <td class="text-left">{{transfer.To}}</td>
          <td class="text-left">{{transfer.ToTime}}</td>
          <td class="text-left">{{transfer.FromTime}}</td>
          <td class="text-left">{{stringParser(transfer.Position).toString()}}</td>
          <td class="text-left"><button class="btn btn-outline-secondary" @click="deleteItem(transfer)">Удалить</button></td>
        </tr>
      </tbody>
  </table>
</div>
  </div>


  <div class="modal fade" id="exampleModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true" ref="exampleModal" >
      <div class="modal-dialog modal-dialog-centered modal-xl">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title" id="exampleModalLabel">{{ModalTitle}}</h5>
            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
          </div>
          <div class="modal-body">
            <div class="m-3">
              <label>Откуда</label>
              <select class="mx-2" v-model="From">
                <option v-for="storage in StorageList" :key="storage.Id">{{storage.Name}}</option>
                <option>Свой вариант</option>
              </select>
            </div>
            <div class="m-3">
              <label>Куда</label>
              <select class="mx-2" v-model="To">
                <option v-for="storage in StorageList" :key="storage.Id">{{storage.Name}}</option>
              </select>
            </div>
            <div class="m-3">
              <label >Дата отправки</label>
              <input class="mx-2" type="date" v-model="ToTime"/>
            </div>
            <div class="m-3">
              <label >Дата прибытия</label>
              <input class="mx-2" type="date" v-model="FromTime"/>
            </div>
            <div class="m-3">
              <label >Выбор товара</label>
              <select class="mx-2" v-model="SelectedItem">
                <option :value="item" v-for="item in ItemList" :key="item.Id">{{item.Name}}</option>
              </select>
            </div>
            <div class="m-3">
              <label >Количество</label>
              <input class="mx-2" type="text" v-model="Counter"/>
            </div>
            <div class="m-3">
              <button class="btn btn-light btn-outline-primary" @click="modalAddButton()">Добавить товар</button>
            </div>
            <div>
              <table class="table table-bordered align-middle">
                <thead>
                  <tr>
                    <th> Товар</th>
                    <th> Количество</th>
                  </tr>
                </thead>
                  <tbody>
                    <tr v-for="selecteditem in SelectedItemList" :key="selecteditem.id">
                      <td class="text-left">{{selecteditem.Name}}</td>
                      <td class="text-left">
                        <input type="text" v-model="selecteditem.Counter"/>
                      </td>
                    </tr>
                  </tbody>
              </table>
            </div>
            <div>
              <button class="btn btn-light btn-outline-primary" @click="sendTransfer()">Отправить товары</button>
            </div>
          </div>
        </div>
      </div>
    </div>
</template>



<script>

export default {
  data(){
      
    return{
      ModalTitle: "Добавить новое перемещение",
      StorageList: [],
      ItemList: [],
      TransferList: [],

      ModalAddOtherTransferWindow: false,

      From: '',
      To: '',

      SelectedItemList: [],
      SelectedItem: '',
      Counter: '',
      ToTime: new Date().toISOString().substr(0, 10),
      FromTime: new Date().toISOString().substr(0, 10),

      TransferItems :{
        From: '',
        To: '',
        ToTime: '',
        FromTime: '',
        Position: ''
      },
    }
  },
  async mounted(){
    await this.refreshData();
    $(this.$refs.exampleModal).on("hidden.bs.modal", this.ModalTransferHiden);
  
  },
  methods:{
    async refreshData(){
      await this.refreshStorage();
      await this.refreshItem();
      await this.refreshTransfer();
    },
    async refreshStorage(){
      const {data} = await this.axios.get(variables.API_URL+ "storage");
      this.StorageList = data;
    },
    async refreshItem(){
      const {data} = await this.axios.get(variables.API_URL+ "nomenclature");
      this.ItemList = data;
    },
    async refreshTransfer(){
      const {data} = await this.axios.get(variables.API_URL+ "transfer");
      this.TransferList = data;
    },
    async deleteItem(item){
      await this.axios.delete(variables.API_URL + 'transfer/' + item.Id);
      this.refreshData();
    },
    ModalTransferHiden(){
      this.SelectedItemList = [],
      this.SelectedItem = '',
      this.Counter = '',
      this.DateTo = new Date().toISOString().substr(0, 10),
      this.DateFrom =  new Date().toISOString().substr(0, 10)
      this.refreshData();
    },
    stringParser(ArrPosition){
      var jsonDict = JSON.parse(ArrPosition);
      let res = [];
      for(var key in jsonDict){
        let name = "";
        var jsonNomenclature = JSON.parse(key);
        name += jsonNomenclature["Name"] + ": " + jsonDict[key];
        res.push(name);
        name = "";
      }
      return res;
    },
    modalAddButton(){
      if(!this.SelectedItemList.some(obj => obj.Id == this.SelectedItem.Id)){
        this.SelectedItem.Counter = this.Counter;
        this.SelectedItemList.push(this.SelectedItem);
      }
      else{
        alert("Данные товар уже в списке");
      }
    },
    async sendTransfer(){
        this.TransferItems.From = this.From;
        this.TransferItems.To = this.To;
        this.TransferItems.ToTime = this.ToTime;
        this.TransferItems.FromTime = this.FromTime;
        this.TransferItems.Position = JSON.stringify(this.SelectedItemList);

        var res = JSON.stringify(this.TransferItems);
        await this.axios.post(variables.API_URL + "transfer/AddTransfer",
        JSON.stringify(res),{headers:{
          'Content-Type' : 'application/json'
        }});
    },
  
  }
};
</script>