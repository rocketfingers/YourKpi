<template>
  <div>
    <v-layout row wrap ma-3>
      <form enctype="multipart/form-data" novalidate>
        <div class="filebox">
          <v-layout row wrap>
            <v-flex xs12>
              <v-layout row wrap justify-space-around mt-2>
                <h3>{{ title }}</h3>
              </v-layout>
            </v-flex>
            <v-flex xs12>
              <input
                ref="fileInputDrag"
                type="file"
                multiple
                name="Drawing"
                :disabled="isSaving"
                @change="
                  changeFilesState($event.target.name, $event.target.files);
                  fileCount = $event.target.files.length;
                "
                class="input-file"
              />
            </v-flex>
            <v-flex xs12>
              <v-layout
                ma-2
                row
                wrap
                v-for="file in selectedFileList"
                :key="file.name"
              >
                <v-tooltip bottom>
                  <template v-slot:activator="{ on }">
                    <v-flex xs12 v-on="on">
                      <v-layout
                        @click="openSelectedFile(file.id)"
                        row
                        wrap
                        mb-4
                        justify-space-around
                      >
                        <v-icon large color="green">fa-file-image</v-icon>
                      </v-layout>
                    </v-flex>

                    <v-flex xs12>
                      <v-layout row wrap justify-space-around>
                        <span>{{ file.nazwa }}{{ file.name }}</span>
                      </v-layout>
                    </v-flex>
                  </template>

                  <span>Otwórz plik {{ file.nazwa }} {{ file.name }}</span>
                </v-tooltip>
              </v-layout>

              <p v-show="selectedFileList.length == 0">
                Przeciągnij plik <br />
                lub kliknij by wyszukać
              </p>
            </v-flex>
            <v-flex xs12>
              <p v-show="isSaving">Uploading {{ fileCount }} files...</p>
            </v-flex>
          </v-layout>
        </div>
      </form>
    </v-layout>
  </div>
</template>

<script>
import fileMixin from '../fileMixin'

export default {
  name: 'NewProduct',
  components: {
  },
  mixins: [fileMixin],
  props: {
    title: String,
    files: Array,
    editMode: Boolean,
    openFileUrl: String
  },
  data () {
    return {
      selectedFileList: [],
      fileCount: 0,
      isSaving: false,
      requiredRule: (v) => !!v || 'To pole jest wymagane',
      numberRule: val => {
        if (val < 0) return 'Wprowadź wartość dodatnią'
        return true
      }
    }
  },
  computed: { },
  watch: {
    selectedFileList (val) {
      this.$emit('editedFiles', this.files)
    },
    files (val) {
      this.assignSelectedFiles(val)
    }
  },
  methods: {
    assignSelectedFiles (val) {
      this.selectedFileList = (val && val.length > 0 && val[0] !== {}) ? val : []
    },
    changeFilesState (fieldName, fileList) {
      this.selectedFileList = fileList
      if (!fileList.length) return
      this.files = this.$refs.fileInputDrag.files
    },
    openSelectedFile (fileId) {
      this.openPdf(this.openFileUrl + fileId)
    }
  },
  created () {
    this.assignSelectedFiles(this.files)
  },
  destroyed () {
  }
}
</script>

<style lang="scss" scoped>
.filebox {
  outline: 2px dashed grey;
  background: lightcyan;
  outline-offset: -12px;
  color: rgb(106, 91, 91);
  padding: 10px 10px;
  min-height: 150px;
  position: relative;
  cursor: pointer;
}
.input-file {
  opacity: 0;
  background: pink;
  margin-top: -18%;
  margin-left: -5%;
  height: 100%;
  width: 100%;

  position: absolute;
  cursor: pointer;
}

.filebox:hover {
  background: lightblue; /* when mouse over to the drop zone, change color */
}

.filebox p {
  text-align: center;
  padding: 45px 0;
  font-size: 1.2em;
}
</style>
