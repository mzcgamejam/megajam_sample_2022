# Document : https://docs.aws.amazon.com/cli/latest/reference/gamelift/upload-build.html
# You need to set an environment variable called GAME_SERVER_PROJECT_PATH in your OS.
PROJECT_PATH="$GAME_SERVER_PROJECT_PATH"
GAME_SERVER_PATH=\\BattleServer\\bin\\Debug
FULL_BUILD_PATH="$PROJECT_PATH""$GAME_SERVER_PATH"

# Set variables to suit your development environment
OS=WINDOWS_2012
BUILD_NAME=BattleServer
BUILD_VERSION=0.0.1
REGION=ap-northeast-2

aws gamelift upload-build \
--operating-system "$OS" \
--build-root "$FULL_BUILD_PATH" \
--name "$BUILD_NAME" \
--build-version "$BUILD_VERSION" \
--region "$REGION"
