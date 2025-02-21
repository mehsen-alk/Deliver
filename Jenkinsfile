pipeline {
    agent any

    stages {
        stage('Clone Repository') {
            steps {
                git branch: 'dev', url: 'https://github.com/mehsen-alk/Deliver.git'
            }
        }

        stage('Build Docker Image') {
            steps {
                sh 'docker build -t deliver.api:latest .'
            }
        }

        stage('Deploy Application') {
            steps {
                sshagent(['Mohsen1@Deliver']) {
                    sh '''
                    ssh root@145.223.101.137 << EOF
                      docker stop deliver.api || true
                      docker rm deliver.api || true
                      docker run -d --name deliver.api deliver.api:latest
                    EOF
                    '''
                }
            }
        }
    }
}

