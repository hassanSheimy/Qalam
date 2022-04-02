<template>
    <div class="profile-page">        
        <profile-header :items="items" />

        <div class="d-flex border-bottom profile-page-content">
            <v-icon color="secondary" size="50">{{page.icon}}</v-icon>
            <span class="profile-page-title ml-2 secondary--text">{{page.title}}</span>
        </div>

        <router-view />
        
        <profile-footer :inset="true" app :fixed="false" />
    </div>
</template>

<script>
import ProfileHeader from '@/components/profile/profile-header.vue'
import ProfileFooter from '@/components/layout/secondary-footer.vue'
import moment from 'moment'

export default {
    name: "Profile",

    components: {
        ProfileHeader,
        ProfileFooter
    },

    data: () => ({}),

    computed: {
        page(){
            var index = this.items.findIndex(i => i.to == this.$route.path);
            
            return index == -1 ? { icon: 'fas fa-info-circle', title: this.$locales.t('info') } : {
                icon: this.items[index].icon,
                title: this.items[index].title
            };
        },
        role(){
            return this.$route.path.toLowerCase().split("/")[2];
        },
        items(){
            var lang = this.$locales.current;
            return this.$locales.t(this.role + 'Profile');
        }
    },

    created(){
        this.CheckAutherity();
        this.GetUserData();
    },

    methods: {
        CheckAutherity(){
            var roles = [];
            if(this.IsThereUser()){
                roles = this.user.roles.find(r => this.$route.path.toLowerCase().split("/")[2]);
            }

            if(!roles || !roles.length){
                this.Unauthorized(this.$route.fullPath);
            }
        },
        GetUserData(){
            this.HttpRequest({
                url: "api/user/personal-data",
                auth: true,
                faildNotify: false
            }).then(this.GetSuccess)
            .catch(this.GetFailed);
        },
        GetSuccess(response){
            if(response.data){
                response.data.birthDate = moment(response.data.birthDate).format("YYYY-MM-DD");
                this.$store.commit("UpdateUserData", response.data);
                this.$eventBus.$emit('user-details-updated');
            }
            else{
                this.Unauthorized(this.$route.fullPath);
            }
        },
        GetFailed(error){
            // error happend
        },
    }
}
</script>

<style scoped>
.profile-page-content{
    margin: 0 30px;
    margin-bottom: 30px
}

.profile-page-title{
    font-size: 40px; 
    color: #293450;
    margin-right: 10px;
}
</style>