def nuget_path = env.NugetPath
def nuget_server = env.HqvNugetServer
def nuget_server_key = env.HqvNugetServerKey
 
node('windows') {

	stage('compile') {	
		checkout scm
		// git 'https://github.com/hqv1/csharp-seedwork.git' 
		bat 'dotnet clean'
		bat 'dotnet restore'
		bat 'dotnet build -c Release'
		stash 'everything'
	}
 
	stage('test') {
		dir("test\Seedwork.Test.Unit") {
			bat 'dotnet restore'
			bat 'dotnet test --filter Category=Unit'
		}				
	}
 
	stage('publish') {	
		unstash 'everything'
		bat 'del /S *.nupkg'
		dir("test\Seedwork") {
			bat 'dotnet pack --no-build -c Release'
		}
		dir("test\Seedwork.App") {
			bat 'dotnet pack --no-build -c Release'
		}		
		dir("test\Seedwork.Web") {
			bat 'dotnet pack --no-build -c Release'
		}
		dir("test\Seedwork.Web.Client") {
			bat 'dotnet pack --no-build -c Release'
		}
		bat "${nuget_path} push **\\*.nupkg ${nuget_server_key} -Source ${nuget_server}"
		archiveArtifacts '**\\*.nupkg'		
	}
}
