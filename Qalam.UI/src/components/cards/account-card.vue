<template>
    <custom-card class="v-card-profile full-width" v-bind="$attrs">
        
        <v-avatar slot="offset" class="mx-auto d-block v-card-offset" size="200">
            
            <v-hover v-if="isProfileCard" v-slot:default="{ hover }">
                <v-img :src="userDetails.imagePath || defaultImage" class="elevation-10 white">
                    <v-expand-transition>
                        <div v-if="hover"
                        class="d-flex transition-fast-in-fast-out primary darken-2 v-card--reveal display-3 white--text"
                        style="height: 100%;">
                            <v-btn icon @click="uploadImage = true">
                                <v-icon class="white--text" size="32" v-html="'fas fa-images'" />
                            </v-btn>
                        </div>
                    </v-expand-transition>
                </v-img>
            </v-hover>
            <v-img v-else :src="userDetails.imagePath || defaultImage" class="elevation-10 white" />
        </v-avatar>
        <v-card-text class="text-center">
            <h6 v-if="isProfileCard" class="title mb-3">{{role}}</h6>
            <h4 class="headline">{{fullName}}</h4>
            <p v-if="$locales.t('teacher') == role || !isProfileCard" class="subtitle-1 mt-3">{{$locales.t('teacherOf')}} : {{userDetails.subjectName}}</p>
            <p class="subtitle-2 font-weight-light"> {{FixLength(userDetails.about)}} </p>
            <v-btn text icon class="social-media" :href="userDetails.facebook" target="_blank"><v-icon>fab fa-facebook-square facebook</v-icon></v-btn>
            <v-btn text icon class="social-media" :href="userDetails.linkedIn" target="_blank"><v-icon>fab fa-linkedin linkedin</v-icon></v-btn>
        </v-card-text>

    </custom-card>
</template>

<script>
import UserDefultImage from '@/assets/images/user.jpg'
import CustomCard from '@/components/cards/custom-card.vue'

export default {
    name: 'AccountCard',

    components: {
        CustomCard,
    },

    props: {
        isProfileCard: {
            type: Boolean,
            default: false
        },
        role: {
            type: String,
            required: false
        },
        userObject: {
            type: Object,
            required: false
        }
    },

    data: () => ({
        userDetails: {
            imagePath: ''
        },
        defaultImage: UserDefultImage,
        uploadImage: false
    }),

    computed: {
        fullName() {
            return this.userDetails.fullName ? this.userDetails.fullName : `${this.userDetails.firstName} ${this.userDetails.lastName}`;
        }
    },

    mounted() {
        if(this.isProfileCard){
            this.userDetails = this.$store.getters.UserDetails;
        }
        else{
            this.userDetails = this.userObject;
        }
    },

    methods: {
        FixLength(text) {
            var editedText = text && text.length > 80 ? text.substr(0, 77) + ' ...' : text;
            return editedText;
        },
    },
}
</script>

<style scoped>
.full-width {
    width: 90%
}
.v-card-profile {
    display: inline-block !important;
    margin-top: 0 !important;
}
.v-card-offset {
    top: unset !important;
    margin-bottom: unset !important;
    margin-top: -50px !important;
    box-shadow: 0 10px 30px -12px rgba(0, 0, 0, 0.42), 0 4px 25px 0px rgba(0, 0, 0, 0.12), 0 8px 10px -5px rgba(0, 0, 0, 0.2) !important;
}
.social-media{
    font-size: 23px;
    margin: 10px;
}
.facebook:hover{
    color: #3b5998
}
.linkedin:hover{
    color:#0073b0
}
.dropdown-toggle::after{
    display: none;
}

.v-card--reveal {
    align-items: center;
    bottom: 0;
    justify-content: center;
    opacity: .5;
    position: absolute;
    width: 100%;
}
</style>